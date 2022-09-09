import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { FetchInstrumentation } from '@opentelemetry/instrumentation-fetch';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import {SemanticResourceAttributes} from "@opentelemetry/semantic-conventions";
import {Resource} from "@opentelemetry/resources";
import {ConsoleSpanExporter, SimpleSpanProcessor} from "@opentelemetry/sdk-trace-base";
import {OTLPTraceExporter} from "@opentelemetry/exporter-trace-otlp-http";
import {TracingServiceName} from "../constants";
import {W3CTraceContextPropagator} from "@opentelemetry/core";

const provider = new WebTracerProvider({
    resource: new Resource({
        [SemanticResourceAttributes.SERVICE_NAME]: TracingServiceName,
    }),
});

provider.addSpanProcessor(new SimpleSpanProcessor(new ConsoleSpanExporter()));
provider.addSpanProcessor(new SimpleSpanProcessor(new OTLPTraceExporter({
    // this is the URL for sending OpenTelemetry instrumentation data (proxied via an Nginx docker container to avoid CORS issues) 
    url: "http://agent-proxy:4418/v1/traces",
})));

provider.register({
    contextManager: new ZoneContextManager(),  // Zone is required to keep async calls in the same trace
    // this is used to propagate OpenTelemetry header (traceparent) to all downstream API calls 
    propagator: new W3CTraceContextPropagator()
});

// this is needed to automatically instrument API calls via fetch()
const fetchInstrumentation = new FetchInstrumentation({
    propagateTraceHeaderCorsUrls: [
        /.+/g,
     ],
    clearTimingResources: true,
});
fetchInstrumentation.setTracerProvider(provider);

// Registering instrumentations
registerInstrumentations({
    instrumentations: [
        fetchInstrumentation,
    ],
});

// @ts-ignore
export default function TraceProvider( {children}): JSX.Element {
    return (
        <>
            {children}
        </>
    );
}