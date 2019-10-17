import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/User/Account/Balance";
import BuyToken from "./Buy";

const TokenBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge id="tokenPopover" color="dark" style={{ width: "100px" }}>
        <Balance currency="token" attribute="available" />
      </Badge>
      <Popover
        style={{
          width: "175px"
        }}
        placement="bottom"
        isOpen={open}
        target="tokenPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader>TOKEN</PopoverHeader>
        <PopoverBody>
          <BuyToken />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default TokenBreadcrumb;
