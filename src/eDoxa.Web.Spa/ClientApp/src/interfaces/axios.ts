import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";

export type AxiosClient = "default" | "stripe" | "leagueOfLegends";

export interface AxiosPayload {
  client?: AxiosClient;
  request: AxiosRequestConfig;
}
export interface AxiosActionCreator<T> {
  types: T[];
  payload: AxiosPayload;
}

export interface AxiosErrorData {
  errors: { [key: string]: string[] };
  status: number;
  title: string;
  traceId: string;
}

export interface AxiosAction<ActionType> {
  type: ActionType;
  payload: AxiosResponse;
  error: AxiosError<AxiosErrorData>;
}
