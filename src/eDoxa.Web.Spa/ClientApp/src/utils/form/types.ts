import { AxiosError } from "axios";
import { AxiosErrorData } from "utils/axios/types";
import { SubmissionError } from "redux-form";

export function throwSubmissionError(
  error: AxiosError<AxiosErrorData>
): void | never {
  if (error.isAxiosError) {
    const { data, status } = error.response;
    switch (status) {
      case 400:
      case 412: {
        delete data.errors[""];
        if (data.errors && !data.errors.length) {
          throw new SubmissionError<AxiosErrorData>(data.errors);
        } else {
          throw new Error("Something went wrong.");
        }
      }
      default: {
        throw new SubmissionError<AxiosErrorData>({
          _error: "Something went wrong. You should try again later ..."
        });
      }
    }
  }
}
