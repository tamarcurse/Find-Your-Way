//path="/newProvider"

import React, { useState } from "react";
import Box from '@mui/material/Box';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';

import PrivateProvider from "./privateProvider/PrivateProvider";
import PrivateFactory from "../factory/privateFactory";


const steps = ['הזנת פרטים אישיים', 'הזנת נתוני המפעל', 'הזנת נתוני החנויות והמשאיות'];
export default function NewProvider() {
    //  const [activeStep, setActiveStep] = React.useState(0);
    const [show, setShow] = useState(true)
    return (
        <>
            <Box className="stepers c-white">
                <Stepper>
                    <Step active={show}>
                        <StepLabel>הזנת פרטים אישיים</StepLabel>

                    </Step>
                    <Step active={!show}>
                        <StepLabel>הזנת נתוני המפעל</StepLabel>

                    </Step>
                    <Step active={false}>
                        <StepLabel>הזנת נתוני החנויות והמשאיות</StepLabel>

                    </Step>
                </Stepper>
            </Box>
            <PrivateProvider show={show} setShow={setShow} />
            <PrivateFactory show={!show} />
        </>
    )
}