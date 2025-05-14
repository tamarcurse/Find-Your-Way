import logo from './logo.svg';
import './App.css';

import Driver from './components/driver/driver';
import store from './redux/store';
import ProviderLogIn from './components/login/provider/provider';
import DriverLogIn from './components/login/driver/driver';
import { Provider } from 'react-redux';
import {
  BrowserRouter as Router,

  Route,
} from "react-router-dom";
import { Switch } from 'react-router-dom/cjs/react-router-dom.min';

import PrivateProvider from './components/provider/privateProvider/PrivateProvider';

import HomeAbout from './components/home/home';
import PrivateFactory from './components/factory/privateFactory';
import UpdateShops from './components/shops/updateShop/updateShop';
import PrivateShop from './components/shops/privateShop/privateShop';
import UpdateTruck from './components/trucks/TrucksAll/trucksAll';
import PrivateTruck from './components/trucks/privateTruck/privateTruck';
import CurrentLocation from './components/driver/map';
// import MapContainer from './components/driver/map3';
import App1 from './components/driver/tryMap';
//import Map from './components/provider/SchedulingMap';
import Map2 from './components/provider/mapProvider';
import key from './components/key';
import Links from './components/links';
import MapProvider from './components/provider/mapProvider';
import LoginNewProvider from './components/login/newProvider/newProvider';
import NavBar from './components/home/navbar';
import NewProvider from './components/provider/NewProvider';
import About from './components/login/about';


function App() {

  return (
    <Provider store={store} >
      <div dir='rtl'>

        <div className='App bg-image'>

          <Router>
            <Switch>


              <Route path="/driver/:licensePlateTruck" children={<Driver></Driver>}></Route>
              <Route path="/login/driver">
                <DriverLogIn></DriverLogIn>
              </Route>
              <Route path="/login/provider">
                <ProviderLogIn></ProviderLogIn>
              </Route>
              <Route path="/provider/privateProvider">
                <PrivateProvider></PrivateProvider>
              </Route>
              <Route path="/provider/privateFactory">
                <PrivateFactory status="UPDATE" ></PrivateFactory>
              </Route>
              <Route path="/provider/shops">
                <UpdateShops></UpdateShops>
              </Route>
              <Route path="/provider/truck/privateTruck/:LicensePlate" children={<PrivateTruck></PrivateTruck>}>

              </Route>
              <Route path="/provider/trucks">
                <UpdateTruck />
              </Route>
              <Route path="/login/newProvider">
                <LoginNewProvider></LoginNewProvider>
              </Route>
              <Route path="/provider/shop/privateShop/:ShopId" children={<PrivateShop></PrivateShop>}>

              </Route>
              <Route path="/provider/map">
                <MapProvider />
              </Route>
              <Route path="/provider">

                <HomeAbout></HomeAbout>
              </Route>
              <Route path="/newProvider">
                <NewProvider />
              </Route>
              <Route path="/">

                <About />
              </Route>
            </Switch>
          </Router>

        </div>
      </div >
    </Provider>
  );
}

export default App;
