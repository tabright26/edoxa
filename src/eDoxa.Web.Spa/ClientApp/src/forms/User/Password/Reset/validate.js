const validate = values => {
  const emailRegExp = new RegExp("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$");
  const passwordRegExp = new RegExp("^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})$");
  const errors = {};
  if (!values.email) {
    errors.email = "Email is required";
  } else if (!emailRegExp.test(values.email)) {
    errors.email = "Invalid email";
  }
  if (!values.password) {
    errors.password = "Password is required";
  } else if (!passwordRegExp.test(values.password)) {
    errors.password = "Invalid password";
  }
  return errors;
};

export default validate;
