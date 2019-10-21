import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Badge from "components/Shared/Badge";
import { withUserEmail } from "store/root/user/email/container";
import { compose } from "recompose";
import Loading from "components/Shared/Loading";

const Email: FunctionComponent<any> = ({
  className,
  email: {
    data: { address, verified },
    error,
    loading
  }
}) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">EMAIL</strong>
      <Badge.Verification className="ml-3 my-auto" verified={verified} />
    </CardHeader>
    <CardBody>
      {loading ? (
        <Loading />
      ) : (
        <dl className="row mb-0">
          <dd className="col-sm-3 mb-0 text-muted">Email</dd>
          <dd className="col-sm-9 mb-0">{address}</dd>
        </dl>
      )}
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(withUserEmail);

export default enhance(Email);
