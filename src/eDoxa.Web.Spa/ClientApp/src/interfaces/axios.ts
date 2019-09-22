import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";

export interface IAxiosPayload {
  client?: "default" | "stripe" | "leagueOfLegends";
  request: AxiosRequestConfig;
}
export interface IAxiosActionCreator<ActionType> {
  types: ActionType[];
  payload: IAxiosPayload;
}
export interface IAxiosAction<ActionType> {
  type: ActionType;
  payload: AxiosResponse;
  error: AxiosError;
}
