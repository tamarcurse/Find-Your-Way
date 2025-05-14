import { applyMiddleware, combineReducers, createStore } from 'redux'
import currentProvider from './reducers/currentProvider';
import CurrentProviderMiddleWare from './middlewers/currentProvider';
import shopsByProvider from './reducers/shopsByProvider';
import TrucksByProvider from './reducers/TrucksByProvider';
import sizeContianByProvider from './reducers/sizeContianByProvider'

const reducer = combineReducers({ currentProvider, shopsByProvider, TrucksByProvider, sizeContianByProvider })
const store = createStore(reducer);
window.store = store;
export default store;
//  applyMiddleware(CurrentProviderMiddleWare)