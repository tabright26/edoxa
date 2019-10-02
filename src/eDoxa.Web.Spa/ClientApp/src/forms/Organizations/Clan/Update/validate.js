const validate = values => {
  const summaryRegExp = new RegExp("^[a-zA-Z0-9- .,]{1,}$");

  const errors = {};

  if (values.summary && !summaryRegExp.test(values.summary)) {
    errors.summary = "Invalid summary";
  }

  return errors;
};

export default validate;
