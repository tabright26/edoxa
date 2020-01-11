import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { VALIDATE_GAME_AUTHENTICATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { Game } from "types";
import { validateGameAuthentication, loadGames } from "store/actions/game";
import { toastr } from "react-redux-toastr";
import authorize from "utils/oidc/AuthorizeService";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {}

interface OutterProps {
  game: Game;
  handleCancel: () => any;
  setAuthenticationFactor: (data: any) => any;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit }) => (
  <Form className="w-100" onSubmit={handleSubmit}>
    <div className="mx-auto w-25">
      <Button.Submit block>Validate</Button.Submit>
    </div>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: VALIDATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (values, dispatch: any, { game }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(validateGameAuthentication(game, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch: any) => {
      dispatch(loadGames()).then(() => {
        console.log(window.location.pathname);
        return authorize
          .getUser()
          .then(user => console.log(user))
          .then(() =>
            authorize
              .signIn({
                returnUrl: window.location.pathname
              })
              .then(() => authorize.getUser().then(x => console.log(x)))
          );
      });
    },
    onSubmitFail: (
      error,
      dispatch,
      submitError,
      { setAuthenticationFactor }
    ) => {
      toastr.error("Error", "Validating game authentication failed.");
      setAuthenticationFactor(null);
    }
  })
);

export default enhance(CustomForm);
