import React from 'react';
import {BarChart, CartesianGrid, ResponsiveContainer, Tooltip, XAxis, YAxis} from 'recharts';
import {WeatherForecast} from "../models/WeatherForecast";
import {getWeatherDetails} from "../services/weatherApiService";


interface WeatherChartProps {
    value: WeatherForecast[]
}



export default class WeatherChart extends React.Component<{}, AppState> {

    constructor(props: {}) {
        super(props);
        this.state = {weatherDetails: []}
    }
    async componentDidMount() {
        const weatherDetails = await getWeatherDetails();
        this.setState({weatherDetails});
    }
    render() {
        return (
            <div>
                <ResponsiveContainer width="100%" aspect={3}>
                    <BarChart
                        width={500}
                        height={300}
                        data={this.state.weatherDetails}
                        margin={{
                            top: 5,
                            right: 30,
                            left: 20,
                            bottom: 5,
                        }}
                    >
                        <CartesianGrid strokeDasharray="3 3"/>
                        <XAxis dataKey="date"/>
                        <YAxis/>
                        <Tooltip/>
                    </BarChart>
                </ResponsiveContainer>
                <button
                    className="refresh"
                    onClick={async () => await this.refresh()}>
                    Refresh
                </button>
            </div>
        );
    }

    private async refresh(): Promise<void> {
        const weatherDetails = await getWeatherDetails();
        this.setState({weatherDetails});
    }

}
