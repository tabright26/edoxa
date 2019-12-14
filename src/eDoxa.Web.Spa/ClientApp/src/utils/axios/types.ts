import { AxiosRequestConfig, AxiosResponse, AxiosError } from "axios";
import { Promise } from "q";
import { Interface } from "readline";

export interface AxiosPayload {
  request: AxiosRequestConfig;
}
export interface AxiosActionCreator<T> {
  types: T[];
  payload: AxiosPayload;
  meta?: AxiosActionCreatorMeta;
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
  meta: AxiosActionMeta;
}

export interface AxiosState<
  AxiosDataState = any,
  AxiosErrorState = string | AxiosError<AxiosErrorData>
> {
  readonly data: AxiosDataState;
  readonly loading: boolean;
  readonly error?: AxiosErrorState;
}

export type Resolve = (result: any) => void;
export type Reject = (error: any) => void;

interface PreviousAction {
  meta: AxiosActionCreatorMeta;
}

export interface AxiosActionMeta {
  previousAction: PreviousAction;
}

export interface AxiosActionCreatorMeta {
  resolve: Resolve | null;
  reject: Reject | null;
}
