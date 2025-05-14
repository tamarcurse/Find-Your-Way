import produce from 'immer'
import axios from 'axios';

const initialState = {
    trucks: null
};

const reducer = produce((state, action) => {
    switch (action.type) {
        case 'UPDATE_ALL_TRUCKS':
            state.trucks = action.payLoad
    }

}, initialState)
export default reducer