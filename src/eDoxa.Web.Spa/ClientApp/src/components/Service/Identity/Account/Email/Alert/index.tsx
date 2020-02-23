import React, { FunctionComponent } from "react";
import { Alert as ReactstrapAlert } from "reactstrap";
import EmailForm from "components/Service/Identity/Account/Email/Form";
import { compose } from "recompose";
import { HocUserProfileEmailVerifiedStateProps } from "utils/oidc/containers/types";
import { withUserProfileEmailVerified } from "utils/oidc/containers/withUserProfileEmailVerified";

type OutterProps = {};

type InnerProps = HocUserProfileEmailVerifiedStateProps;

type Props = OutterProps & InnerProps;

const Alert: FunctionComponent<Props> = ({ emailVerified }) => {
  const date = new Date();
  const timestamp = date.setMinutes(date.getMinutes() - 15);
  const resendDate = localStorage.getItem("RESEND_EMAIL_BUTTON_CLICKED_AT");
  return (
    !emailVerified && (
      <ReactstrapAlert className="bg-primary border-primary mt-4 mb-0 d-flex">
        <div className="d-inline my-auto text-uppercase">
          Please verify your account
        </div>
        {!(resendDate && timestamp < JSON.parse(resendDate)) && (
          <EmailForm.Resend className="d-inline" />
        )}
      </ReactstrapAlert>
    )
  );
};

const enhance = compose<InnerProps, OutterProps>(withUserProfileEmailVerified);

export default enhance(Alert);
