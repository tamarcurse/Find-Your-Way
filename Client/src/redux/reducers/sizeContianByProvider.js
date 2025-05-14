import produce from 'immer'

const initialState = {
    sizeContain: [{}]
};

const reducer = produce((state, action) => {
    switch (action.type) {
        case 'UPDATE_ALL_SIZE_CONTAIN':
            state.sizeContain = action.payLoad
    }

}, initialState)
export default reducer