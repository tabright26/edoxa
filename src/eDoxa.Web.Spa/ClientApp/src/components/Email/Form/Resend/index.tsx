import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { RESEND_EMAIL_FROM } from "utils/form/constants";
import { compose } from "recompose";
import { throwSubmissionError } from "utils/form/types";
import { Gender } from "types";
import { resendUserEmail } from "store/actions/identity";
import { AxiosActionCreatorMeta } from "utils/axios/types";

type FormData = {
  firstName: string;
  lastName: string;
  gender: Gender;
};

type InnerProps = InjectedFormProps<FormData, Props>;

type OutterProps = { className?: string };

type Props = InnerProps & OutterProps;

const Resend: FunctionComponent<Props> = ({
  handleSubmit,
  submitting,
  className
}) => (
  <Form onSubmit={handleSubmit} className="ml-auto">
    <Button.Submit
      color="dark"
      loading={submitting}
      size="sm"
      className={className}
    >
      Resend
    </Button.Submit>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: RESEND_EMAIL_FROM,
    onSubmit: async (_values, dispatch: any) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(resendUserEmail(meta)).then(() =>
            localStorage.setItem(
              "RESEND_EMAIL_BUTTON_CLICKED_AT",
              JSON.stringify(Date.now())
            )
          );
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    }
  })
);

export default enhance(Resend);
