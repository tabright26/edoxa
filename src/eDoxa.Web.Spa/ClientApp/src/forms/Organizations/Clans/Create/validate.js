const validate = values => {
  const nameRegExp = new RegExp("^(Canada)|(United States)$");
  const summaryRegExp = new RegExp("^[a-zA-Z0-9- .,]{1,}$");

  const errors = {};

  if (!values.name) {
    errors.name = "Name is required";
  } else if (!nameRegExp.test(values.name)) {
    errors.name = "Invalid name";
  }

  if (values.summary && !summaryRegExp.test(values.summary)) {
    errors.summary = "Invalid summary";
  }
  return errors;
};

export default validate;
