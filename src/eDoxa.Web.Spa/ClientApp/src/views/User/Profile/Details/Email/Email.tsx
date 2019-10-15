import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Badge from "components/Shared/Override/Badge";
import { withUserEmail } from "store/root/user/email/container";
import { compose } from "recompose";

const Email: FunctionComponent<any> = ({ className, email, emailVerified }) => (
  <Card className={className}>
    <CardHeader>
      <strong>EMAIL</strong>
      <Badge.Verification className="ml-3" verified={emailVerified} />
    </CardHeader>
    <CardBody>
      <dl className="row mb-0">
        <dd className="col-sm-3 mb-0 text-muted">Email</dd>
        <dd className="col-sm-9 mb-0">{email}</dd>
      </dl>
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(withUserEmail);

export default enhance(Email);
