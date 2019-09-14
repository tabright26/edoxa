const validate = values => {
  /*const ccNameRegExp = new RegExp("^$");
  const ccNumberRegExp = new RegExp("^$");
  const ccMonthRegExp = new RegExp("^$");
  const ccYearRegExp = new RegExp("^$");
  const ccCVVRegExp = new RegExp("^$");*/

  const errors = {};
  /*
  if (!values.ccName) {
    errors.ccName = "Credit card name is required";
  } else if (!ccNameRegExp.test(values.ccName)) {
    errors.ccName = "Invalid credit card name";
  }

  if (!values.ccNumber) {
    errors.ccNumber = "Credit card number is required";
  } else if (!ccNumberRegExp.test(values.ccNumber)) {
    errors.ccNumber = "Invalid credit card number";
  }

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

  if (!values.ccCVV) {
    errors.ccCVV = "Credit card CVV is required";
  } else if (!ccCVVRegExp.test(values.ccCVV)) {
    errors.ccCVV = "Invalid credit card CVV";
  }*/

  return errors;
};

export default validate;
