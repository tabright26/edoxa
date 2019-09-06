const validate = values => {
  const errors = {};

  var emailReg = new RegExp("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$");

  if (values.email) {
    var res = emailReg.test(values.email);
    if (!res) {
      errors.email = "Invalid email";
    }
  } else {
    errors.email = "Email is required";
  }

  return errors;
};

export default validate;
