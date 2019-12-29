import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import Badge from "components/Shared/Badge";
import { withUserEmail } from "store/root/user/email/container";
import { compose } from "recompose";
import Loading from "components/Shared/Loading";

const Email: FunctionComponent<any> = ({
  className,
  email: { data, error, loading }
}) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">EMAIL</strong>
      {data && (
        <Badge.Verification className="ml-3 my-auto" verified={data.verified} />
      )}
    </CardHeader>
    <CardBody>
      {loading ? (
        <Loading />
      ) : data ? (
        <dl className="row mb-0">
          <dd className="col-sm-3 mb-0 text-muted">Email</dd>
          <dd className="col-sm-9 mb-0">{data.address}</dd>
        </dl>
      ) : (
        <span>Not found</span>
      )}
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(withUserEmail);

export default enhance(Email);
