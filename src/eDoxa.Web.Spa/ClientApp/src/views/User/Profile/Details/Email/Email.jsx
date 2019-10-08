import React from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import { connectUserEmail } from "store/user/email/container";

const EmailCard = ({ className, email }) => (
  <Card className={className}>
    <CardHeader>
      <strong>EMAIL</strong>
    </CardHeader>
    <CardBody>
      <dl className="row mb-0">
        <dd className="col-sm-3 mb-0 text-muted">Email</dd>
        <dd className="col-sm-9 mb-0">{email}</dd>
      </dl>
    </CardBody>
  </Card>
);

export default connectUserEmail(EmailCard);