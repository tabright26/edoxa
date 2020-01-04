import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { UPDATE_USER_DOXATAG_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { changeUserDoxatag } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  doxatagSpecialRegex,
  DOXATAG_MINIMUM_LENGTH,
  DOXATAG_MAXIMUM_LENGTH,
  DOXATAG_REQUIRED,
  DOXATAG_LENGTH_UNDER_INVALID,
  DOXATAG_LENGTH_OVER_INVALID,
  DOXATAG_INVALID
} from "validation";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import produce, { Draft } from "immer";
import { UserDoxatag } from "types";

interface FormData {
  name: string;
}

interface StateProps {}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const ReduxForm: FunctionComponent<Props> = ({
  error,
  handleSubmit,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Field
      type="text"
      name="name"
      label="Name"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  Props,
  RootState
> = state => {
  const { data } = state.root.user.doxatagHistory;
  const doxatags = produce(data, (draft: Draft<UserDoxatag[]>) => {
    draft.sort((left: UserDoxatag, right: UserDoxatag) =>
      left.timestamp < right.timestamp ? 1 : -1
    );
  });
  return {
    initialValues: doxatags[0] || null
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: UPDATE_USER_DOXATAG_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise(async (resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(changeUserDoxatag(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.name) {
        errors.name = DOXATAG_REQUIRED;
      } else if (values.name.length < DOXATAG_MINIMUM_LENGTH) {
        errors.name = DOXATAG_LENGTH_UNDER_INVALID;
      } else if (values.name.length > DOXATAG_MAXIMUM_LENGTH) {
        errors.name = DOXATAG_LENGTH_OVER_INVALID;
      } else if (!doxatagSpecialRegex.test(values.name)) {
        errors.name = DOXATAG_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(ReduxForm);
