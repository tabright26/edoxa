import React, { FunctionComponent } from "react";
import { FormGroup, Form, Button } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import { UNLINK_GAME_CREDENTIAL_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { Game } from "types";
import { unlinkGameCredential } from "store/actions/game";
import { throwSubmissionError } from "utils/form/types";
import authorizeService from "utils/oidc/AuthorizeService";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type OutterProps = {
  game: Game;
  handleCancel: () => void;
};

type Props = InnerProps & OutterProps;

const Unlink: FunctionComponent<Props> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup className="mb-0">
    <Button type="submit" size="sm" color="primary" className="mr-2">
        Yes
      </Button>
      <Button onClick={() => handleCancel()} size="sm">
        No
      </Button>
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: UNLINK_GAME_CREDENTIAL_FORM,
    onSubmit: async (_values, dispatch, { game }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(unlinkGameCredential(game, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: _result => {
      authorizeService.signIn({
        returnUrl: window.location.pathname
      });
    }
  })
);

export default enhance(Unlink);
