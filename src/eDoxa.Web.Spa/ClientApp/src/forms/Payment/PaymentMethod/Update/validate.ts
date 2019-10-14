const validate = values => {
  const ccMonthRegExp = new RegExp("^(01|02|03|04|05|06|07|08|09|10|11|12)$");
  const ccYearRegExp = new RegExp("^[2][0][1-9][0-9]$");

  const errors: any = {};

  if (!values.ccMonth) {
    errors.ccMonth = "Credit card expiration month is required";
  } else if (!ccMonthRegExp.test(values.ccMonth)) {
    errors.ccMonth = "Invalid credit card expiration month";
  }

  if (!values.ccYear) {
    errors.ccYear = "Credit card expiration year is required";
  } else if (!ccYearRegExp.test(values.ccYear)) {
    errors.ccYear = "Invalid credit card expiration year";
  }

  return errors;
};

export default validate;
