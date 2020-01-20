import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { UPDATE_USER_DOXATAG_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { changeUserDoxatag } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  DOXATAG_REGEXP,
  DOXATAG_MIN_LENGTH,
  DOXATAG_MAX_LENGTH,
  DOXATAG_REQUIRED,
  DOXATAG_MIN_LENGTH_INVALID,
  DOXATAG_MAX_LENGTH_INVALID,
  DOXATAG_INVALID
} from "utils/form/validators";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import produce, { Draft } from "immer";
import { UserDoxatag } from "types";
import authorizeService from "utils/oidc/AuthorizeService";

interface FormData {
  name: string;
}

interface StateProps {}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  error,
  handleSubmit,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="name"
      placeholder="Name"
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
    onSubmitSuccess: (result, dispatch, { handleCancel }) => {
      handleCancel();
      authorizeService.signIn({
        returnUrl: window.location.pathname
      });
    },
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.name) {
        errors.name = DOXATAG_REQUIRED;
      } else if (values.name.length < DOXATAG_MIN_LENGTH) {
        errors.name = DOXATAG_MIN_LENGTH_INVALID;
      } else if (values.name.length > DOXATAG_MAX_LENGTH) {
        errors.name = DOXATAG_MAX_LENGTH_INVALID;
      } else if (!DOXATAG_REGEXP.test(values.name)) {
        errors.name = DOXATAG_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
