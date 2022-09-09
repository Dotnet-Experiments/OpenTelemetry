import {WeatherForecast} from "../models/WeatherForecast";

export interface AppState {
    weatherDetails: WeatherForecast[]
}

export const initialState: AppState = { weatherDetails: []};
