import React from "react";
import axios from "axios";
export const Client = axios.create({
    baseURL: "https://localhost:44345/api"
});