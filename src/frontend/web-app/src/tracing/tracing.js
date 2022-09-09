import {trace} from '@opentelemetry/api';
import {TracingServiceName} from "../constants";

export function getTracer() {
    return trace.getTracer(TracingServiceName);
}
