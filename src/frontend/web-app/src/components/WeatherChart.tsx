import React, {useEffect} from 'react';
import {Bar, BarChart, CartesianGrid, ResponsiveContainer, Tooltip, XAxis, YAxis} from 'recharts';
import {WeatherForecast} from "../models/WeatherForecast";
import {getWeatherDetails} from "../services/WeatherApiService";
import {context, trace} from "@opentelemetry/api";
import {getTracer} from "../tracing/tracing";


const WeatherChart = () => {
    const [weatherDetails, setWeatherDetails] = React.useState<Array<WeatherForecast>>([]);
    const tracer = getTracer();
    const [parentSpan, setParentSpan] = React.useState<any>(null);
    
    const fetchWeatherData  = async () => {

        const rootSpan = tracer.startSpan("fetch-weather-data");
        
        //const data = await getWeatherDetails();
        
        await context.with(trace.setSpan(context.active(), rootSpan), async () => {
            const data = await getWeatherDetails();
            // @ts-ignore
            trace.getSpan(context.active()).addEvent('fetch-weather-data-completed');
            setWeatherDetails(data);
        });

        rootSpan.end();
    }

    useEffect(() => {
        const data = fetchWeatherData()
            .catch(console.error);
    }, []);
        
    return (
        <div>
            <ResponsiveContainer width="100%" aspect={3}>
                <BarChart
                    width={500}
                    height={300}
                    data={weatherDetails}
                    margin={{
                        top: 5,
                        right: 30,
                        left: 20,
                        bottom: 5,
                    }}
                >
                    <CartesianGrid strokeDasharray="3 3"/>
                    <XAxis dataKey="date"/>
                    <YAxis />
                    <Tooltip/>
                    <Bar dataKey="temperatureC" fill="#8884d8" />
                </BarChart>
            </ResponsiveContainer>
            <button
                className="refresh"
                onClick={ () => fetchWeatherData().catch(console.error)}>
                Refresh
            </button>
        </div>
    );
}

export default WeatherChart;

