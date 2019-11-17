import React, { FunctionComponent } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";

interface Props {
  readonly className?: string;
  readonly canRegister: boolean;
}

const ChallengeRegister: FunctionComponent<Props> = ({
  className,
  canRegister
}) => (
  <>
    {canRegister && (
      <Card className={className}>
        <ChallengeForm.Register className="h-100 w-100" />
      </Card>
    )}
  </>
);

export default ChallengeRegister;
