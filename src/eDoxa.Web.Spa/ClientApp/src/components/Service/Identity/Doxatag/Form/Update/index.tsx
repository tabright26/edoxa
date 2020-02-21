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
import authorizeService from "utils/oidc/AuthorizeService";
import { Doxatag } from "types/identity";
import { WorkflowProps } from "views/Workflow";

type FormData = {
  name: string;
};

type StateProps = {};

type OutterProps = WorkflowProps & {
  handleCancel: () => void;
};

type InnerProps = InjectedFormProps<FormData, Props> & StateProps;

type Props = InnerProps & OutterProps;

const Update: FunctionComponent<Props> = ({
  error,
  handleSubmit,
  submitting,
  anyTouched,
  nextWorkflowStep
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field
      type="text"
      name="name"
      placeholder="Name"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} className="mr-2" size="sm">
        Save
      </Button.Submit>
      {nextWorkflowStep && (
        <Button.Link
          className="float-right"
          size="sm"
          onClick={() => nextWorkflowStep()}
        >
          Skip
        </Button.Link>
      )}
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  Props,
  RootState
> = state => {
  const { data } = state.root.user.doxatagHistory;
  const doxatags = produce(data, (draft: Draft<Doxatag[]>) => {
    draft.sort((left: Doxatag, right: Doxatag) =>
      left.timestamp < right.timestamp ? 1 : -1
    );
  });
  const doxatag = doxatags[0] || null;
  return {
    initialValues: doxatag
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
    onSubmitSuccess: (
      _result,
      _dispatch,
      { handleCancel, nextWorkflowStep }
    ) => {
      if (nextWorkflowStep) {
        nextWorkflowStep();
      } else {
        handleCancel();
      }
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

export default enhance(Update);
