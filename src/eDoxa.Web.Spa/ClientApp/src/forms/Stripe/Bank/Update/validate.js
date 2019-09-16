const validate = values => {
  const accountHolderNameRegExp = new RegExp("[a-zA-Z -]{1,15}$");

  const errors = {};

  if (!values.account_holder_name) {
    errors.account_holder_name = "Account holder card name is required";
  } else if (!accountHolderNameRegExp.test(values.account_holder_name)) {
    errors.account_holder_name = "Invalid account holder name";
  }

  return errors;
};

export default validate;
