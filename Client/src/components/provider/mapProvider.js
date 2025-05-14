//path="provider/map"

import React, { useState } from "react";
import { useEffect } from "react";

import { useParams } from "react-router-dom";
import { Client } from "../Client"
import { connect } from "react-redux";

import key from "../key";

import Map from "./SchedulingMap";

import HaveNotSchedulingTruck from "./HaveNotSchedulingTruck";
import NavBar from "../home/navbar";

function mapStateToProps(state) {
    return {
        shops: state.shopsByProvider.shops
        , currentProvider: state.currentProvider,
        trucks: state.trucks 
        
    }
}
export default connect(mapStateToProps)(
    function MapProvider(props) {

        const [load, set_load] = useState(false);
        const [load2, set_load2] = useState(false);

        let { licensePlateTruck } = useParams() || '5378'
        const { shops, currentProvider, dispatch, trucks } = props
        const providerId = currentProvider.providerPrivate.ProviderId || "326238821"
        const [shopsByProvider, setShopsByProvider] = useState()
        const [isStation, setIsStatuion] = useState(true)
        const [statDic, setStateDic] = useState(null)
        const arrayColor = [
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
        
        useEffect(async function render1() {


            let res = await Client.get("/StationsInShop/GetStationInMap?providerId=" + providerId);
    
            set_load(true);
            if (res.data.length == 0) {
                setIsStatuion(false)
                return
            }
            setStateDic(res.data)
        }
            , [])
 
        if (!isStation)
            return (<HaveNotSchedulingTruck></HaveNotSchedulingTruck>)

        return (
            <>
                <NavBar />
                <div className="c-white">מפת המסלולים </div>
                {/* <div>{stationsInShop}</div> */}
                <div className="flex">
                    {(statDic && load && Object.keys(statDic).length != 0) ?
                        <>
                            <div className="map">
                                {/* <img src={map}/> */}
                                <Map

                                    googleMapURL={
                                        'https://maps.googleapis.com/maps/api/js?key=' +
                                        key +
                                        '&libraries=geometry,drawing,places'
                                    }
                                    markers={statDic}
                                    loadingElement={<div style={{ height: `100%` }} />}
                                    containerElement={<div style={{ height: "80vh" }} />}
                                    mapElement={<div style={{ height: `100%` }} />}
                                    //defaultCenter={{ "lat": stationsInShop[0].latitude, "lng": stationsInShop[0].longitude }}
                                    defaultCenter={null}
                                    defaultZoom={11}
                                ></Map>
                            </div>

                            <div className="bg-white m-auto">
                 
                                <ul>

                                    {statDic.map((s, index) => (

                                        <li key={index}><a href={"/driver/" + s.truck.LicensePlate} style={{ color: arrayColor[index % arrayColor.length] }}>הכנס כאן לפירוט אודות המסלול בצבע זה</a>
                                        </li>


                                    ))}
                                </ul>
                            </div>
                        </>
                        : <div>loading...</div>}</div>
            </>


        )
    })















