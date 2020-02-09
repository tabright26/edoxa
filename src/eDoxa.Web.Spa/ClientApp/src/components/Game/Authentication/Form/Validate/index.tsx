import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { VALIDATE_GAME_AUTHENTICATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { validateGameAuthentication } from "store/actions/game";
import { toastr } from "react-redux-toastr";
import authorizeService from "utils/oidc/AuthorizeService";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { throwSubmissionError } from "utils/form/types";
import { GameOptions } from "types";

interface FormData {}

interface OutterProps {
  gameOptions: GameOptions;
  handleCancel: () => any;
  setAuthenticationFactor: (data: any) => any;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Validate: FunctionComponent<Props> = ({ handleSubmit, submitting }) => (
  <Form className="w-100" onSubmit={handleSubmit}>
    <div className="mx-auto w-25">
      <Button.Submit loading={submitting} block>
        Validate
      </Button.Submit>
    </div>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: VALIDATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (_values, dispatch, { gameOptions: gameOption }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(validateGameAuthentication(gameOption.name, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, _dispatch, { gameOptions }) => {
      authorizeService
        .signIn({
          returnUrl: window.location.pathname
        })
        .then(() => {
          toastr.success(
            "Game credentials linked",
            `Your ${gameOptions.displayName} credentials have been successfully linked.`
          );
        });
    },
    onSubmitFail: (
      _error,
      _dispatch,
      _submitError,
      { setAuthenticationFactor }
    ) => {
      toastr.error("Error", "Summoner name validation failed.");
      setAuthenticationFactor(null);
    }
  })
);

export default enhance(Validate);
