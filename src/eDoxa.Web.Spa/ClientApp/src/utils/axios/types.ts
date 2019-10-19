import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";

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

export interface AxiosAction<ActionType, TData = any> {
  type: ActionType;
  payload: AxiosResponse<TData>;
  error: AxiosError<AxiosErrorData>;
}

export interface AxiosState<AxiosDataState = any, AxiosErrorState = string | AxiosError<AxiosErrorData>> {
  readonly data: AxiosDataState;
  readonly loading: boolean;
  readonly error?: AxiosErrorState;
}
