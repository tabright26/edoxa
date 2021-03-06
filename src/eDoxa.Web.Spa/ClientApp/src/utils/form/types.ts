import { AxiosError } from "axios";
import { AxiosErrorData } from "utils/axios/types";
import { SubmissionError } from "redux-form";
import {
  FieldValidationRule,
  FIELD_VALIDATION_RULE_TYPE_REQUIRED,
  FIELD_VALIDATION_RULE_TYPE_REGEX,
  FIELD_VALIDATION_RULE_TYPE_LENGTH,
  FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH,
  FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH
} from "types";
import format from "string-format";

export function getFieldValidationRuleMessage(
  validationRules: FieldValidationRule[],
  value: string
): string | undefined {
  validationRules = validationRules
    .filter(validationRule => validationRule.enabled)
    .sort((left, right) => (left.order < right.order ? -1 : 1));
  for (let index = 0; index < validationRules.length; index++) {
    const validationRule = validationRules[index];
    if (
      (validationRule.type === FIELD_VALIDATION_RULE_TYPE_REQUIRED && !value) ||
      (validationRule.type === FIELD_VALIDATION_RULE_TYPE_REGEX &&
        !new RegExp(validationRule.value).test(value)) ||
      (validationRule.type === FIELD_VALIDATION_RULE_TYPE_LENGTH &&
        !value.length) ||
      (validationRule.type === FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH &&
        value.length < validationRule.value) ||
      (validationRule.type === FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH &&
        value.length > validationRule.value)
    ) {
      return format(validationRule.message, validationRule.value);
    } else {
      return undefined;
    }
  }
}

export function throwSubmissionError(
  error: AxiosError<AxiosErrorData> | stripe.Error | any
): void | never {
  if (error && error.isAxiosError) {
    const { data, status } = error.response;
    switch (status) {
      case 400:
      case 412: {
        delete data.errors[""];
        if (data.errors && !data.errors.length) {
          throw new SubmissionError<AxiosErrorData>(data.errors);
        } else {
          throw new SubmissionError<AxiosErrorData>({
            _error: "Something went wrong. You should try again later ..."
          });
        }
      }
      default: {
        throw new SubmissionError<AxiosErrorData>({
          _error: "Something went wrong. You should try again later ..."
        });
      }
    }
  } else {
    throw new SubmissionError<AxiosErrorData>({
      _error: error.message
    });
  }
}
