
/* global google */
import React, { useEffect, useState } from 'react';
import {
    withGoogleMap,
    GoogleMap,
    withScriptjs,
    Marker,
    DirectionsRenderer,
} from 'react-google-maps';

class MapDirectionsRenderer extends React.Component {

    constructor(props) {
        super(props);

    }
    state = {
        directions: null,
        error: null,
    };

    f() {
        //alert('map direct');
        const { places, travelMode } = this.props;
        const waypoints = places.map((p) => ({
            location: { lat: p.latitude, lng: p.longitude },
            stopover: true,
        }));
        const origin = waypoints.shift().location;
        const destination = waypoints.pop().location;

        const directionsService = new google.maps.DirectionsService();
        directionsService.route(
            {
                origin: origin,
                destination: destination,
                travelMode: travelMode,
                waypoints: waypoints,
            },
            (result, status) => {
                if (status === google.maps.DirectionsStatus.OK) {
                    this.setState({
                        directions: result,
                    });
                } else {
                    this.setState({ error: result });
                }
            }
        );
    }

    componentDidMount() {
        this.f();


    }

    render() {
      
        if (this.state.error) {
            return <>

                <h1>{this.state.error}</h1>
            </>;
        }
        else
            return (
                <>

                    {
                        (this.state.directions ?
                            <DirectionsRenderer directions={this.state.directions}
                                options={{
                                    polylineOptions: {
                                        strokeOpacity: 0.5,
                                        strokeColor: this.props.color,
                                    }
                                }} /> : <h1>no directions</h1>)
                    }
                </>


            )
    }
}

const Map =
    withScriptjs(
        withGoogleMap((props) => {
            const [markersNew, setMaekersNew] = useState()
       
            const colors = [
                "green",
                "orange",
                "purple",
                "red",
                "white",
                "yellow",
                "black",
                "blue",
                "brown",
                "shadow",
                "gray"

            ]
            let iconMarker = new window.google.maps.MarkerImage(
                "http://labs.google.com/ridefinder/images/mm_20_gray.png",
                null, /* size is determined at runtime */
                null, /* origin is 0,0 */
                null, /* anchor is bottom center of the scaled image */
                new window.google.maps.Size(32, 32)
            );
            // useEffect(() => {
            //     for (const key in object) {
            //         if (Object.hasOwnProperty.call(object, key)) {
            //             const element = object[key];

            //         }
            //     }

            // }, [])
            return (
                <GoogleMap
                    defaultCenter={props.defaultCenter}
                    defaultZoom={props.defaultZoom}
                >
                    {
                        props.markers && props.markers.map(function (truck, index) {
                            truck.stations && truck.stations.map((marker, i) => {
                                const position = { lat: marker.latitude, lng: marker.longitude };
                                return <Marker key={index * 100 + i} position={position}
                                    icon={iconMarker} //options={{ icon: `http://labs.google.com/ridefinder/images/mm_20_gray.png` }} //icon="http://labs.google.com/ridefinder/images/mm_20_gray.png"//{"http://labs.google.com/ridefinder/images/" + arrayColor[index] + ".png"} 
                                />
                            })
                        })
                    }
                    {
                        props.markers && props.markers.map(function (truck, index) {
                            debugger
                            return <MapDirectionsRenderer key={index}
                                places={truck.stations}
                                travelMode={google.maps.TravelMode.DRIVING}
                                
                                color={colors[index % 9]}
                            />
                        })

                    }


                 

                </GoogleMap>
            )
        })
    );

export default Map;














// import React from "react";
// import {
//     withGoogleMap,
//     GoogleMap,
//     withScriptjs,
//     Marker,
//     DirectionsRenderer
// } from "react-google-maps";

// class MapDirectionsRenderer extends React.Component {
//     state = {
//         directions: null,
//         error: null
//     };

//     componentDidMount() {
//         const { places, travelMode } = this.props;

//         const waypoints = places.map(p => ({
//             location: { lat: p.latitude, lng: p.longitude },
//             stopover: true
//         }))
//         const origin = waypoints.shift().location;
//         const destination = waypoints.pop().location;



//         const directionsService = new google.maps.DirectionsService();
//         directionsService.route(
//             {
//                 origin: origin,
//                 destination: destination,
//                 travelMode: travelMode,
//                 waypoints: waypoints
//             },
//             (result, status) => {
//                 if (status === google.maps.DirectionsStatus.OK) {
//                     this.setState({
//                         directions: result
//                     });
//                 } else {
//                     this.setState({ error: result });
//                 }
//             }
//         );
//     }

//     render() {
//         if (this.state.error) {
//             return <h1>{this.state.error}</h1>;
//         }
//         return (this.state.directions && <DirectionsRenderer directions={this.state.directions} />)
//     }
// }

// const Map = withScriptjs(
//     withGoogleMap(props => (
//         <GoogleMap
//             defaultCenter={props.defaultCenter}
//             defaultZoom={props.defaultZoom}
//         >
//             {props.markers.map((marker, index) => {
//                 const position = { lat: marker.latitude, lng: marker.longitude };
//                 return <Marker key={index} position={position} />;
//             })}
//             <MapDirectionsRenderer
//                 places={props.markers}
//                 travelMode={google.maps.TravelMode.DRIVING}
//             />
//         </GoogleMap>
//     ))
// );

// export default Map;