import {WeatherForecast} from "../models/WeatherForecast";
import axios from "axios";

const apiServer = "localhost"; // "api1"

export async function getWeatherDetails(): Promise<WeatherForecast[]> {
    const apiServer = "api1";

    try {
        //const {data} = await axios.get<Array<WeatherForecast>>(`http://${apiServer}:8081/WeatherForecast`);
        const response = await fetch(`http://${apiServer}:8081/WeatherForecast`, {
            method: 'GET',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            }
        });
        
        let data;
        if(response && response.ok) {
            data = await response.json();
        }
        
        console.log(`Data: ${JSON.stringify(data)}`);
        return data;
    } catch (error) {

    } finally {
    }
    return [];
}