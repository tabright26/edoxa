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
    if (validationRule.type === FIELD_VALIDATION_RULE_TYPE_REQUIRED) {
      if (!value) {
        return format(validationRule.message, validationRule.value);
      }
    }
    if (validationRule.type === FIELD_VALIDATION_RULE_TYPE_REGEX) {
      if (!new RegExp(validationRule.value).test(value)) {
        return format(validationRule.message, validationRule.value);
      }
    }
    if (validationRule.type === FIELD_VALIDATION_RULE_TYPE_LENGTH) {
      if (!value.length) {
        return format(validationRule.message, validationRule.value);
      }
    }
    if (validationRule.type === FIELD_VALIDATION_RULE_TYPE_MIN_LENGTH) {
      if (value.length < validationRule.value) {
        return format(validationRule.message, validationRule.value);
      }
    }
    if (validationRule.type === FIELD_VALIDATION_RULE_TYPE_MAX_LENGTH) {
      if (value.length > validationRule.value) {
        return format(validationRule.message, validationRule.value);
      }
    }
    return undefined;
  }
}

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
