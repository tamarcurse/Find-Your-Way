//path=/driver/:LicensePlate
import React, { useState } from "react";
import { useEffect } from "react";
// import axios from "axios";
import { useParams } from "react-router-dom";
import { Client } from "../Client"
import { connect } from "react-redux";
import { updateShops } from "../../redux/actions";
import key from "../key";
import axios from "axios";
// import Map from "./tryMap/map";
import Map from './tryMap/map'
import { Redirect } from "react-router-dom";
import HaveNotTruck from "./haveNotTruck";
import { ListGroup } from "react-bootstrap";
import { ListGroupItem } from "react-bootstrap";
import { Box } from "@mui/material";
import { Card } from "react-bootstrap";
import OneTruckInTable from "../trucks/oneTruckInTable/oneTruckInTable";
import truckDriverImg from "../../images/truck.png"
import "../../App.css"
function mapStateToProps(state) {
  return {
    shops: state.shopsByProvider.shops 
    , currentProvider: state.currentProvider,
     
    }
  }

export default connect(mapStateToProps)(
  function Driver(props) {

    const [load, set_load] = useState(false);
    const [load2, set_load2] = useState(false);

    let { licensePlateTruck } = useParams() || '4567'
    const [stationsInShop, setStationsInShop] = useState(null);
    const { shops, currentProvider, dispatch } = props
    const providerId = currentProvider.providerPrivate.ProviderId || "326238821"
    const [shopsByProvider, setShopsByProvider] = useState()
    //const [places, setPlaces] = useState([])
    const [isStation, setIsStatuion] = useState(true)
    const [sumGoodsRequired, setSumGoodsRequired] = useState(0)

    const [currentTruck, setcurrentTruck] = useState(null)
    useEffect(async function render1() {
      //let stations = [];
      let res = await Client.get("/StationsInShop/GetStationOfDriver?licensePlateTruck=" + licensePlateTruck)
      set_load(true);
      if (res.data.length == 0) {
        setIsStatuion(false)
        return
      }
      // else {


      setStationsInShop(res.data)
      //let res2 = await 
      Client.get("/Truck/getById?id=" + licensePlateTruck).then((res2 => {
        if (res2.data != "")
          setcurrentTruck(res2.data)
      }))




      let sum = 0
      res.data.forEach(element => {
        sum += element.GoodsRequired
      });
      setSumGoodsRequired(sum)

      // }

    }, [])
    if (!isStation)
      return (<HaveNotTruck></HaveNotTruck>)

    return (
      <>
        <div className="c-white fixed">המסלול שלך</div>
        {/* <div>{stationsInShop}</div> */}
        <div className="flex">
          {(stationsInShop && load && Object.keys(stationsInShop).length != 0) ?
            <>
              {/* <div className="flex"> */}
              <div className="map m-5">
                <Map

                  googleMapURL={
                    'https://maps.googleapis.com/maps/api/js?key=' +
                    key +
                    '&libraries=geometry,drawing,places'
                  }
                  markers={stationsInShop}
                  loadingElement={<div style={{ height: `100%` }} />}
                  containerElement={<div style={{ height: "80vh" }} />}
                  mapElement={<div style={{ height: `100%` }} />}
                  //defaultCenter={{ "lat": stationsInShop[0].latitude, "lng": stationsInShop[0].longitude }}
                  defaultCenter={null}
                  defaultZoom={11}

                ></Map>

              </div>
              <div>
                {(currentTruck) && <>
                  <div className="cards">
                    <div numbered className=" container cards">
                      <Card className="card truckPrivate">
                        <div className="me-auto">

                          <Card.Img variant="top" src={truckDriverImg} />
                          {/* <span>נהג: </span> */}
                          <Card.Body>
                            <Card.Title className="fw-bold">
                              לוחית רישוי:
                              {currentTruck.LicensePlate}
                            </Card.Title>


                            <Card.Text>
                              שם הנהג:
                              {currentTruck.NameOfDriver}
                            </Card.Text>
                            <Card.Text>
                              ת.ז.:
                              {currentTruck.IdOfDriver}
                            </Card.Text>


                          </Card.Body>




                        </div>
                      </Card></div></div>
                </>}

                <h2 className="header2">התחנות שלך</h2>
                <ListGroup as="ol" numbered>
                  {stationsInShop.map((s, index) => (
                    <ListGroup.Item key={index}>
                      <h4>
                        {String.fromCharCode(65 + index)}
                      </h4>
                      <div>

                        {
                          (s.TimeStart) ? s.TimeStart + "-" + s.TimeFinish :
                            (index == 0) ?
                              "יציאה מהמפעל" : "חזרה למפעל"

                        }


                      </div>
                      <div>{s.shopName}</div>
                      <div>{s.shopAddress}</div>

                      {
                        (s.TimeStart) ?

                          <div> כמות הסחורה הנדרשת: {s.GoodsRequired}</div> :
                          (index == 0) ?
                            <div> כמות הסחורה לטעינה :
                              {s.GoodsRequired}
                            </div> :
                            <div></div>
                      }
                    </ListGroup.Item>

                  ))}

                </ListGroup></div>
              {/* <div>{sumGoodsRequired} :סה"כ סחורה</div> */}

              {/* </div> */}
            </>
            : <div>loading...</div>
          }</div>

      </>


    )
  })
