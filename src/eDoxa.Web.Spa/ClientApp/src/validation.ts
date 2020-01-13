import { sentenceCase } from "change-case";

export const DOXATAG_REGEXP = new RegExp(
  "^[a-zA-Z0-9][a-zA-Z0-9_ -]{0,14}[a-zA-Z0-9_-]$"
);
export const DOXATAG_REQUIRED = sentenceCase("DoxaTag is required");
export const DOXATAG_INVALID = sentenceCase(
  "Allowed characters are upper/lower case letters, numbers, -, _ and space."
);
export const DOXATAG_MIN_LENGTH = 2;
export const DOXATAG_MIN_LENGTH_INVALID = sentenceCase(
  `DoxaTag must have at least ${DOXATAG_MIN_LENGTH} characters (Min. ${DOXATAG_MIN_LENGTH})`
);
export const DOXATAG_MAX_LENGTH = 16;
export const DOXATAG_MAX_LENGTH_INVALID = sentenceCase(
  `DoxaTag must be less or equal to ${DOXATAG_MAX_LENGTH} characters (Max. ${DOXATAG_MAX_LENGTH})`
);
export const PROFILE_FIRST_NAME_REGEXP = new RegExp(
  "^[A-Z]((-)[a-zA-Z]|[a-z]){1,15}$"
);
export const PROFILE_FIRST_NAME_REQUIRED = sentenceCase(
  "First name is required"
);
export const PROFILE_FIRST_NAME_INVALID = sentenceCase("Invalid first name");
export const PROFILE_LAST_NAME_REGEXP = new RegExp(
  "^[A-Z]((-)[a-zA-Z]|[a-z]){1,15}$"
);
export const PROFILE_LAST_NAME_REQUIRED = sentenceCase("Last name is required");
export const PROFILE_LAST_NAME_INVALID = sentenceCase("Invalid last name");
export const PROFILE_GENDER_REQUIRED = sentenceCase("Gender is required");
export const PROFILE_GENDER_INVALID = sentenceCase("Invalid day");
export const PROFILE_DOB_YEAR_REGEXP = new RegExp("^[0-9]{4}$");
export const PROFILE_DOB_YEAR_REQUIRED = sentenceCase(
  "Year of birth is required"
);
export const PROFILE_DOB_YEAR_INVALID = sentenceCase("Invalid year");
export const PROFILE_DOB_MONTH_REGEXP = new RegExp("^(0[1-9]|1[012])$");
export const PROFILE_DOB_MONTH_REQUIRED = sentenceCase(
  "Month of birth is required"
);
export const PROFILE_DOB_MONTH_INVALID = sentenceCase("Invalid month");
export const PROFILE_DOB_DAY_REGEXP = new RegExp("^(0[1-9]|[12]\\d|3[01])$");
export const PROFILE_DOB_DAY_REQUIRED = sentenceCase(
  "Day of birth is required"
);
export const PROFILE_DOB_DAY_INVALID = sentenceCase("Invalid day");
export const PHONE_REGEXP = new RegExp("^[0-9]{11}$");
export const PHONE_REQUIRED = sentenceCase("Phone is required");
export const PHONE_INVALID = sentenceCase("Phone is invalid");
export const EMAIL_REGEXP = new RegExp(
  "^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$"
);
export const EMAIL_REQUIRED = sentenceCase("Email is required");
export const EMAIL_INVALID = sentenceCase("Email is invalid");
export const PASSWORD_REGEXP = new RegExp(
  "^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})$"
);
export const PASSWORD_REQUIRED = sentenceCase("Password is required");
export const PASSWORD_INVALID = sentenceCase("Password is invalid");
export const ADDRESS_COUNTRY_REGEXP = new RegExp("^(CA)|(US)$");
export const ADDRESS_COUNTRY_REQUIRED = sentenceCase("Country is required");
export const ADDRESS_COUNTRY_INVALID = sentenceCase("Invalid country");
export const ADDRESS_LINE1_REGEXP = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
export const ADDRESS_LINE1_REQUIRED = sentenceCase("Main address is required");
export const ADDRESS_LINE1_INVALID = sentenceCase("Invalid main address");
export const ADDRESS_LINE2_REGEXP = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
export const ADDRESS_LINE2_INVALID = sentenceCase("Invalid secondary address");
export const ADDRESS_CITY_REGEXP = new RegExp("^[a-zA-Z- ]{1,}$");
export const ADDRESS_CITY_REQUIRED = sentenceCase("City is required");
export const ADDRESS_CITY_INVALID = sentenceCase("Invalid city");
export const ADDRESS_STATE_REGEXP = new RegExp("^[a-zA-Z- ]{1,}$");
export const ADDRESS_STATE_INVALID = sentenceCase("Invalid state");
export const ADDRESS_POSTAL_CODE_REGEXP = new RegExp("^[0-9A-Z]{5,6}$");
export const ADDRESS_POSTAL_CODE_INVALID = sentenceCase("Invalid postal code");
export const PAYMENT_METHOD_CARD_EXP_MONTH_REGEXP = new RegExp(
  "^(01|02|03|04|05|06|07|08|09|10|11|12)$"
);
export const PAYMENT_METHOD_CARD_EXP_MONTH_REQUIRED = sentenceCase(
  "Credit card expiration month is required"
);
export const PAYMENT_METHOD_CARD_EXP_MONTH_INVALID = sentenceCase(
  "Invalid credit card expiration month"
);
export const PAYMENT_METHOD_CARD_EXP_YEAR_REGEXP = new RegExp(
  "^[2][0][1-9][0-9]$"
);
export const PAYMENT_METHOD_CARD_EXP_YEAR_REQUIRED = sentenceCase(
  "Credit card expiration year is required"
);
export const PAYMENT_METHOD_CARD_EXP_YEAR_INVALID = sentenceCase(
  "Invalid credit card expiration year"
);
