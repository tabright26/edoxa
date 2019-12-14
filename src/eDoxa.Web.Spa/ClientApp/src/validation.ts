export const DOXATAG_MINIMUM_LENGTH = 2;
export const DOXATAG_MAXIMUM_LENGTH = 16;

const DOXATAG_SPECIAL_REGEX = "^[a-zA-Z0-9_ -]{2,16}$";
export const doxatagSpecialRegex = new RegExp(DOXATAG_SPECIAL_REGEX);

export const DOXATAG_REQUIRED = "Doxatag is required";
export const DOXATAG_LENGTH_UNDER_INVALID =
  "DoxaTag must have at least 2 characters (Min. 2)";
export const DOXATAG_LENGTH_OVER_INVALID =
  "DoxaTag must be less or equal to 16 characters (Max. 16)";
export const DOXATAG_INVALID =
  "Allowed characters are upper/lower case letters, numbers, -, _ and space.";

//-------------------------------------------------------------------------------

const PERSONALINFO_NAME_REGEX = "^[A-Z]((-)[a-zA-Z]|[a-z]){1,15}$";
export const personalInfoNameRegex = new RegExp(PERSONALINFO_NAME_REGEX);

const PERSONALINFO_YEAR_REGEX = "^[0-9]{4}$";
export const personalInfoYearRegex = new RegExp(PERSONALINFO_YEAR_REGEX);

const PERSONALINFO_MONTH_REGEX = "^(0[1-9]|1[012])$";
export const personalInfoMonthRegex = new RegExp(PERSONALINFO_MONTH_REGEX);

const PERSONALINFO_DAY_REGEX = "^(0[1-9]|[12]\\d|3[01])$";
export const personalInfoDayRegex = new RegExp(PERSONALINFO_DAY_REGEX);

export const PERSONALINFO_FIRSTNAME_REQUIRED = "First name is required";
export const PERSONALINFO_FIRSTNAME_INVALID = "Invalid first name";

export const PERSONALINFO_LASTNAME_REQUIRED = "Last name is required";
export const PERSONALINFO_LASTNAME_INVALID = "Invalid last name";

export const PERSONALINFO_YEAR_REQUIRED = "Year of birth is required";
export const PERSONALINFO_YEAR_INVALID = "Invalid year";

export const PERSONALINFO_MONTH_REQUIRED = "Month of birth is required";
export const PERSONALINFO_MONTH_INVALID = "Invalid month";

export const PERSONALINFO_DAY_REQUIRED = "Day of birth is required";
export const PERSONALINFO_DAY_INVALID = "Invalid day";

export const PERSONALINFO_GENDER_REQUIRED = "Gender is required";
export const PERSONALINFO_GENDER_INVALID = "Invalid day";

//-------------------------------------------------------------------------------

const EMAIL_REGEX =
  "^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$";
export const emailRegex = new RegExp(EMAIL_REGEX);

const PASSWORD_REGEX = "^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})$";
export const passwordRegex = new RegExp(PASSWORD_REGEX);

export const EMAIL_REQUIRED = "Email is required";
export const EMAIL_INVALID = "Email is invalid";

export const PASSWORD_REQUIRED = "Password is required";
export const PASSWORD_INVALID = "Password is invalid";

//-------------------------------------------------------------------------------

const COUNTRY_REGEX = new RegExp("^(CA)|(US)$");
export const countryRegex = new RegExp(COUNTRY_REGEX);

const LINE1_REGEX = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
export const line1Regex = new RegExp(LINE1_REGEX);

const LINE2_REGEX = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
export const line2Regex = new RegExp(LINE2_REGEX);

const CITY_REGEX = new RegExp("^[a-zA-Z- ]{1,}$");
export const cityRegex = new RegExp(CITY_REGEX);

const STATE_REGEX = new RegExp("^[a-zA-Z- ]{1,}$");
export const stateRegex = new RegExp(STATE_REGEX);

const POSTAL_REGEX = new RegExp("^[0-9A-Z]{5,6}$");
export const postalRegex = new RegExp(POSTAL_REGEX);

export const COUNTRY_REQUIRED = "Country is required";
export const COUNTRY_INVALID = "Invalid country";

export const LINE1_REQUIRED = "Main address is required";
export const LINE1_INVALID = "Invalid main address";

export const LINE2_INVALID = "Invalid secondary address";

export const CITY_REQUIRED = "City is required";
export const CITY_INVALID = "Invalid city";

export const STATE_INVALID = "Invalid state";

export const POSTAL_INVALID = "Invalid postal code";

//-------------------------------------------------------------------------------
