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
    state = {
        directions: null,
        error: null,
    };

    componentDidMount() {
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

    render() {
        if (this.state.error) {
            return <h1>{this.state.error}</h1>;
        }
        return (
            this.state.directions && (
                <DirectionsRenderer directions={this.state.directions} />
            )
        );
    }
}

let position = null
const Map = withScriptjs(
    withGoogleMap((props) => {
        return (
            <GoogleMap
                defaultCenter={props.defaultCenter}
                defaultZoom={props.defaultZoom}>
            
                <MapDirectionsRenderer
                    places={props.markers}
                    travelMode={google.maps.TravelMode.DRIVING}
                />
            </GoogleMap>
        )})
);

export default Map;













