variable "ec2_ami" {
  default = "ami-0c1c30571d2dae5c9"
}

variable "ec2_instance_type" {
  default = "t2.micro"
}

variable "ec2_count" {
  type    = number
  default = 1
}

variable "ec2_sg" {
  default = ["sg-008fecdc0471fe360"] //
}


variable "ec2_subnet_id" {
  default = "subnet-09aea05f4ab73bc68" //
}

variable "ec2_vpc_id" {
  default = "vpc-0c7fcc842735d06aa" //
}
