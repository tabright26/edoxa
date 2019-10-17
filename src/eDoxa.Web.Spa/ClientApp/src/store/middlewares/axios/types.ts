import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";
import { SubmissionError } from "redux-form";

export interface AxiosPayload {
  request: AxiosRequestConfig;
}
export interface AxiosActionCreator<T> {
  types: T[];
  payload: AxiosPayload;
}

export interface AxiosErrorData {
  status: number;
  title: string;
  errors?: { [key: string]: string[] };
  traceId?: string;
}

export interface AxiosAction<ActionType> {
  type: ActionType;
  payload: AxiosResponse;
  error: AxiosError<AxiosErrorData>;
}

export interface AxiosState<AxiosDataState = any, AxiosErrorState = string | AxiosError<AxiosErrorData>> {
  readonly data: AxiosDataState;
  readonly loading: boolean;
  readonly error?: AxiosErrorState;
}

export function throwAxiosSubmissionError(error: AxiosError<AxiosErrorData>): void {
  const { isAxiosError, response } = error;
  if (isAxiosError) {
    const { data, status } = response;
    switch (status) {
      case 400: {
        delete data.errors[""];
        if (data.errors && !data.errors.length) {
          throw new SubmissionError<AxiosErrorData>(response.data.errors);
        } else {
          throw new SubmissionError<AxiosErrorData>({ _error: data.title });
        }
      }
      case 404: {
        break;
      }
      case 415:
      default: {
        throw new SubmissionError<AxiosErrorData>({ _error: "Something went wrong. You should try again later ..." });
      }
    }
  }
}
