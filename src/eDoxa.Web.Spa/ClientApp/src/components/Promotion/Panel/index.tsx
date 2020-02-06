import React, { FunctionComponent } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import PromotionForm from "components/Promotion/Form";

interface Props {
  className?: string;
}

const Panel: FunctionComponent<Props> = ({ className = null }) => (
  <Card className={`card-accent-primary ${className}`}>
    <CardHeader className="d-flex">
      <strong className="text-uppercase my-auto">PROMOTIONAL CODE</strong>
    </CardHeader>
    <CardBody>
      <dl className="row mb-0">
        <dd className="col-sm-3 mb-0 text-muted">Promotional code</dd>
        <dd className="col-sm-5 mb-0">
          <PromotionForm.Redeem />
        </dd>
      </dl>
    </CardBody>
  </Card>
);

export default Panel;
