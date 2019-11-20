import React, { FunctionComponent, useState, useEffect } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";
import authorizeService from "utils/oidc/AuthorizeService";
import { User } from "oidc-client";

interface Props {
  readonly className?: string;
  readonly canRegister: boolean;
}

const ChallengeRegister: FunctionComponent<Props> = ({
  className,
  canRegister
}) => {
  const [user, setUser] = useState<User>(null);
  useEffect(() => {
    authorizeService.getUser().then((user: User) => setUser(user));
  }, []);
  return (
    <>
      {canRegister && user && (
        <Card className={className}>
          <ChallengeForm.Register
            userId={user["sub"]}
            className="h-100 w-100"
          />
        </Card>
      )}
    </>
  );
};

export default ChallengeRegister;
