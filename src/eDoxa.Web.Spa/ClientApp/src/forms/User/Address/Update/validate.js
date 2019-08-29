const validate = values => {
  const errors = {};

  var lineOneReg = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
  var lineTwoReg = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
  var cityReg = new RegExp("^[a-zA-Z- ]{1,}$");
  var stateReg = new RegExp("^[a-zA-Z- ]{1,}$");
  var postalReg = new RegExp("^[0-9A-Z]{5,6}$");

  if (values.line1) {
    var res = lineOneReg.test(values.line1);
    if (!res) {
      errors.line1 = "Invalid main address";
    }
  } else {
    errors.line1 = "Main address is required";
  }

  if (values.line2) {
    res = lineTwoReg.test(values.line2);
    if (!res) {
      errors.line2 = "Invalid secondary address";
    }
  }

  if (values.city) {
    res = cityReg.test(values.city);
    if (!res) {
      errors.city = "Invalid city";
    }
  } else {
    errors.city = "City is required";
  }

  if (values.state) {
    res = stateReg.test(values.state);
    if (!res) {
      errors.state = "Invalid state";
    }
  }

  if (values.postalCode) {
    res = postalReg.test(values.postalCode);
    if (!res) {
      errors.postalCode = "Invalid postal code";
    }
  }

  return errors;
};

export default validate;
