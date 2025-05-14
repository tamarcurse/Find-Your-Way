import React from "react";
import { connect } from "react-redux";
import TextField from '@mui/material/TextField';
function mapStateToProps(state) {
    return { trucks: state.TrucksByProvider.trucks }
}
export default connect(mapStateToProps)(
    function FilterAddressShop(props) {
        const { setArrTrucks, trucks } = props

        function filterkey(e) {

            setArrTrucks(trucks.filter(s => s.IdOfDriver.includes(e)))

        }
        return (
            <div className="margin-4">

                <TextField type="search" onChange={(e) => { filterkey(e.target.value) }} id="outlined-search" label="חיפוש ת.ז. של נהג המשאית" variant="outlined"></TextField>
            </div>
        )
    })