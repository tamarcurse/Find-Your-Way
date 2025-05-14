import produce from 'immer'
import axios from 'axios';
const initialState = {
    shops: null
};

const reducer = produce((state, action) => {
    switch (action.type) {
        case 'UPDATE_ALL_SHOP':
            state.shops = action.payLoad
            break
        case 'UPDATE_ONE_SHOP':

            state.shop = state.shops.filter(s => s.ShopId != action.payLoad.ShopId)
            state.shop.push(action.payLoad)
    }

}, initialState)
export default reducer