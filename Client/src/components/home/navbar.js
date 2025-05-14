//navbar
//path="/provider"
import React, { useState } from "react";
import { Link } from "react-router-dom";
import { Navbar, Nav, Container } from "react-bootstrap";
import "./home.css"
import logo from "../../images/logo.png"
import { Redirect } from "react-router-dom/cjs/react-router-dom.min";
export default function NavBar(props) {
    const { Mypage } = props
    const [privateFactory, setPrivateFactory] = useState(false)
    const [shops, setShops] = useState(false)
    const [trucks, setTrucks] = useState(false)
    const [map, setMap] = useState(false)
    const [home, setHome] = useState(false)


    if (privateFactory)
        return <Redirect to="/provider/privateFactory" />
    if (shops)
        return <Redirect to="/provider/shops/updateShop" />
    if (trucks)
        return <Redirect to="/provider/trucks" />
    if (map)
        return <Redirect to="/provider/map" />
    if (home)
        return <Redirect to="/provider" />

    return (
        <>

            <Navbar id="basic-navbar-nav" bg="dark" variant="dark" sticky="top" inverse fluid>
                <Container className="myNav">
                    <Navbar.Brand onClick={() => setHome(true)}>
                        <img id="logo" src={logo}></img>

                    </Navbar.Brand>
                    <Nav pullRight className="me-auto" >
                        <Nav.Link onClick={() => setPrivateFactory(true)} className={Mypage == "privateFactory" ? "boldNav" : ""}>פרטי מפעל</Nav.Link>
                        <Nav.Link onClick={() => setShops(true)} className={Mypage == "updateShop" ? "boldNav" : ""}>חנויות</Nav.Link>
                        <Nav.Link onClick={() => setTrucks(true)} className={Mypage == "trucks" ? "boldNav" : ""}>משאיות</Nav.Link>
                        <Nav.Link onClick={() => setMap(true)} className={Mypage == "map" ? "boldNav" : ""}>מסלולים</Nav.Link>
                        {/* <button onClick={() => { setRed(true) }}>try</button> */}
                    </Nav>
                </Container>
            </Navbar>
        </>
    )
}
