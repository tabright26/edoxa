import { AxiosError } from "axios";
import { AxiosErrorData } from "utils/axios/types";
import { SubmissionError } from "redux-form";

export function throwSubmissionError(error: AxiosError<AxiosErrorData>): void {
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
