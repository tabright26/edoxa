import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";

export interface IAxiosPayload {
  client?: "default" | "stripe" | "leagueOfLegends";
  request: AxiosRequestConfig;
}
export interface IAxiosActionCreator<TActionType> {
  types: TActionType[];
  payload: IAxiosPayload;
}
export interface IAxiosAction<TActionType> {
  type: TActionType;
  payload: AxiosResponse;
  error: AxiosError;
}
