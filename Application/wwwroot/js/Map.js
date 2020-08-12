var trasa = $("#trail").val();
var GPXfeatures = (new ol.format.GPX()).readFeatures(trasa, { featureProjection: 'EPSG:3857' })

var defaultStyle = {
    'Point': new ol.style.Style({
        image: new ol.style.Circle({
            fill: new ol.style.Fill({
                color: 'rgba(255,255,0,0.5)'
            }),
            radius: 5,
            stroke: new ol.style.Stroke({
                color: '#ff0',
                width: 1
            })
        })
    }),
    'LineString': new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: '#f00',
            width: 3
        })
    }),
    'Polygon': new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(0,255,255,0.5)'
        }),
        stroke: new ol.style.Stroke({
            color: [20, 59, 153, 0.8],
            width: 1
        })
    }),
    'MultiPoint': new ol.style.Style({
        image: new ol.style.Circle({
            fill: new ol.style.Fill({
                color: 'rgba(255,0,255,0.5)'
            }),
            radius: 5,
            stroke: new ol.style.Stroke({
                color: '#f0f',
                width: 1
            })
        })
    }),
    'MultiLineString': new ol.style.Style({
        stroke: new ol.style.Stroke({
            color: '#0f0',
            width: 3
        })
    }),
    'MultiPolygon': new ol.style.Style({
        fill: new ol.style.Fill({
            color: 'rgba(0,0,255,0.5)'
        }),
        stroke: new ol.style.Stroke({
            color: '#00f',
            width: 1
        })
    })
};

var styleFunction = function (feature, resolution) {
    var featureStyleFunction = feature.getStyleFunction();
    if (featureStyleFunction) {
        return featureStyleFunction.call(feature, resolution);
    } else {
        return defaultStyle[feature.getGeometry().getType()];
    }
};

var gpxLayer = new ol.layer.Vector({
    source: new ol.source.Vector({

    }),
    style: styleFunction,
    name: "Moj"
});
gpxLayer.getSource().addFeatures(GPXfeatures);

var iconFeature = new ol.Feature({
    geometry: new ol.geom.Point([])
});

var source = new ol.source.Vector({
    features: [iconFeature]
});
var vector = new ol.layer.Vector({
    source: source
});

var positionFeature = new ol.Feature();

iconFeature.setStyle(new ol.style.Style({
    image: new ol.style.Circle({
        radius: 6,
        fill: new ol.style.Fill({
            color: '#3399CC'
        }),
        stroke: new ol.style.Stroke({
            color: '#fff',
            width: 2
        })
    })
}));

var map = new ol.Map({
    target: 'map',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.BingMaps({
                key: "AmGGdzDzxaRCbL4Ydl1ZyXxGwd8mWOERnX9JvPBjdcnxJeJCgORtbReue12dFstG",
                imagerySet: "AerialWithLabels"
            })
        }),
        vector
    ],

    view: new ol.View({
        center: [2158581.678773377, 6803507.013606967],
        zoom: 10,
    })
});

var MojGpx = (new ol.format.GPX()).readFeatures(trasa, { featureProjection: 'EPSG:3857' })
console.log(MojGpx);
var warstwaGPX = new ol.layer.Vector({
    source: new ol.source.Vector({

    }),
    style: styleFunction,
    name: "Moj"

});
warstwaGPX.getSource().addFeatures(MojGpx);
map.addLayer(warstwaGPX);


map.getView().setCenter(ol.proj.transform([@find[1].Value, @find[0].Value], 'EPSG:4326', 'EPSG:3857'))


var geolocation = new ol.Geolocation({
    tracking: true,
    enableHighAccuracy: true,
    projection: map.getView().getProjection()
});

$("#geolocalization").click(() => {

    var coord = geolocation.getPosition();
    iconFeature.getGeometry().setCoordinates(coord);
    map.getView().setCenter([2140236.791984935, 6836527.8098261645]);
    map.getView().setZoom(6);

    geolocation.on('change', function (evt) {
        var coord = geolocation.getPosition();
        iconFeature.getGeometry().setCoordinates(coord);
        map.getView().setCenter(coord);
        window.console.log(geolocation.getPosition());
        window.console.log(geolocation.getAltitude());
        window.console.log(geolocation.getAltitude());
        window.console.log(geolocation.getAltitudeAccuracy());
        window.console.log(geolocation.getHeading());
    });
});

$("#leadTo").click(() => {


    let nearest = '//router.project-osrm.org/nearest/v1/driving/';
    let route = '//router.project-osrm.org/route/v1/driving/';
    let distance = 0;
    const pointsMap = [];
    const distanceNadRoute = [];
    var durationBetweenPoints = [];
    var durationPoints;

    const promise = {
        getNearest: function (coord) {
            var modify = promise.modifyCoordinate(coord);
            return new Promise(function (resolve, reject) {
                //make sure the coord is on street
                fetch(nearest + modify.join()).then(function (response) {
                    // Convert to JSON
                    return response.json();
                }).then(function (json) {
                    if (json.code === 'Ok') {
                        pointsMap.push(json.waypoints[0].location);
                        pointsMap.push(json.waypoints[0].distance);
                        pointsMap.push(json.waypoints[0].name);
                        resolve(json.waypoints[0].location);
                    } else {
                        alert('Nie otrzymano odpowiedzi z serwera');
                        reject();
                    }
                }).catch(function () {
                    alert('Nie otrzymano odpowiedzi z serwera');
                });
            });
        },
        modifyCoordinate: function (coord) {
            return transform([
                parseFloat(coord[0]), parseFloat(coord[1])
            ], 'EPSG:3857', 'EPSG:4326');

        }
    };

    const styles = {
        route: new ol.style.Style({
            stroke: new ol.style.Stroke({
                width: 8,
                color: [20, 59, 153, 0.8]
            })
        }),


    };

    const drawIcon = function (coord) {
        const marker = new Feature({
            type: 'place',
            geometry: new Point(fromLonLat(coord))
        });
        marker.setStyle(styles.icon);
        that.path.getSource().addFeature(marker);
        return marker;
    };

    const drawRoad = function (polyline) {
        const route = new Polyline({
            factor: 1e5
        }).readGeometry(polyline, {
            dataProjection: 'EPSG:4326',
            featureProjection: 'EPSG:3857'
        });
        const marker = new Feature({
            name: 'route',
            geometry: route
        });
        marker.setStyle(styles.route);
        that.path.getSource().addFeature(marker);
    };

    //  promise.getNearest(e.coordinate).then(function (coord_street) {

    /*  that.points.push(pointsMap);

      drawIcon(coord_street);

      if (that.points.length < 2) {
          return;
      }

      const point1 = that.points[0][0]
      const point2 = that.points[1][0] */
    window.console.log(@find[1].Value + " " +  @find[0].Value)
window.console.log(ol.proj.transform(geolocation.getPosition(), 'EPSG:3857', 'EPSG:4326'))



                        /*    $.getJSON( 'http://router.project-osrm.org/route/v1/driving/13.388860,52.517037;13.397634,52.529407;13.428555,52.523219?overview=false', function( data ) {
                              var items = [];
                              $.each( data, function( key, val ) {
                                  items.push("<li id='" + key + "'>" + val + "</li>");
                                  window.console.log(val);
                              });

                              $( "<ul/>", {
                                "class": "my-new-list",
                                html: items.join( "" )
                              }).appendTo("body");

                            }); */

                /*    fetch('https://router.project-osrm.org/route/v1/driving/13.388860,52.517037;13.397634,52.529407;13.428555,52.523219?overview=false').then((r) => {
                        return r.json();
                    }).then((json) => {

                        if (json.code !== 'Ok') {
                            return;
                        }
                        window.console.log(json.routes[0]);
                    })


                        fetch('https://router.project-osrm.org/route/v1/driving/' +  + ';' + ol.proj.transform(geolocation.getPosition(), 'EPSG:3857','EPSG:4326')).then(function (r) {
                            return r.json();
                        }).then(function (json) {
                            if (json.code !== 'Ok') {
                                return;
                            }

                            drawRoad(json.routes[0].geometry); */

                          /*  durationPoints = json.routes[0].duration;
                            durationBetweenPoints.push(durationPoints);
                            distance = Math.round(json.routes[0].distance / 1000);
                            distanceNadRoute.push(json.routes[0].geometry);
                            distanceNadRoute.push(distance);
                            that.points.push(distanceNadRoute);
                            that.points.push(durationBetweenPoints);
                            that.$store.dispatch("map/addToRoadPoints", that.points);
                            that.points.shift();
                            that.points.pop(); */
                       // });
                    });


            //    });